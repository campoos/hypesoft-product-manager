import './Categorias.css';
import { useEffect, useState } from 'react';

import { fetchCategorias } from "../../services/categories.ts";
import type { CategoriaResponse } from "../../services/categories";

import Header from '../../components/layout/header/Header.tsx'
import Sidebar from '../../components/layout/sidebar/Sidebar.tsx'
import EditCategoriaModal from '../../components/forms/editCategoria/editCategoriaModal.tsx';
import CategoriaModal from '../../components/forms/categoria/CategoriaModal.tsx';

import { useLocation, useNavigate } from "react-router-dom";

import createIcon from "../../assets/plus.png"

import keycloak, { type KeycloakTokenParsed } from '../../auth/keycloak';

export default function Categorias() {

   const [role, setRole] = useState<string | null>(null);

    
    useEffect(() => {
        if (keycloak.authenticated) {
            const token: KeycloakTokenParsed | undefined = keycloak.tokenParsed;

            const roles = token?.realm_access?.roles || [];

            if (roles.includes("administrador")) {
                setRole("administrador");
            } else if (roles.includes("usuario")) {
                setRole("usuario");
            } else {
                setRole("guest");
            }
        }
    }, []);

  const [categorias, setCategorias] = useState<CategoriaResponse[]>([]);

  useEffect(() => {
    fetchCategorias().then(setCategorias);
  }, []);

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    if (location.state?.openProductId) {
      setSelectedCategoriaId(location.state.openProductId);
      setIsEditModalOpen(true);

      navigate(location.pathname, { replace: true });
    }
  }, [location.state, navigate]);

  const [selectedCategoriaId, setSelectedCategoriaId] = useState<string | null>(null);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const [isModalOpen, setIsModalOpen] = useState(false);  

  return (
    <div className="dashboard">
        <CategoriaModal
            isOpen={isModalOpen}
            onClose={() => setIsModalOpen(false)}
            onSuccess={() => {
                fetchCategorias().then(setCategorias);
            }}
        />
        <EditCategoriaModal
          categoriaId={selectedCategoriaId}
          isOpen={isEditModalOpen}
          onClose={() => setIsEditModalOpen(false)}
          onSuccess={() => {
            fetchCategorias().then(setCategorias);
          }}
        />
        {(isModalOpen || isEditModalOpen) && (
          <div className='dark-background'></div>
        )}

      <Header></Header>
      <div className="body-container">

        <Sidebar></Sidebar>

        <main className='main-container'>
          <div className="main-content">
            <header>
                <h1>Categorias</h1>
                <button onClick={() => setIsModalOpen(true)} disabled={role !== "administrador"}>
                    <img src={createIcon} alt="" />
                    <span>Criar</span>
                </button>
            </header>
            
            <div className="tabela-container">
              <table className="tabela">
                <thead className='header-container'>
                  <tr className='header-line'>
                    <th className="categoria-id">Categoria ID</th>
                    <th className="categoria-nome">Categoria</th>
                  </tr>
                </thead>
                <tbody>
                  
                  {categorias.length === 0 && (
                    <tr className='linha-mensagem'>
                      <td colSpan={5} className="tabela-mensagem">Não existem categorias a serem listados</td>
                    </tr>
                  )}

                  {categorias.map(categoria => (
                    <tr
                      key={categoria.id}
                      className={`linha-produto ${role !== "administrador" ? 'disabled' : ''}`} 
                      onClick={() => {
                        if(role !== "administrador"){
                          return
                        }
                        setSelectedCategoriaId(categoria.id);
                        setIsEditModalOpen(true);
                      }}
                    >
                      <td className='id'>{categoria.id}</td>
                      <td className='nome'>{categoria.nome}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}