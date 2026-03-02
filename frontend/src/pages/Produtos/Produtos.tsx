import './Produtos.css';
import { useEffect, useState } from 'react';

import { fetchProductsFiltered } from '../../services/products.ts';
import type { ProductResponse } from '../../services/products.ts';

import Header from '../../components/layout/header/Header.tsx'
import Sidebar from '../../components/layout/sidebar/Sidebar.tsx'
import EditProductModal from '../../components/forms/editProduct/EditProductModal.tsx';
import ProductModal from '../../components/forms/product/ProductModal.tsx';

import { useLocation, useNavigate } from "react-router-dom";

import createIcon from "../../assets/plus.png"

export default function Produtos() {

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    if (location.state?.openProductId) {
      setSelectedProductId(location.state.openProductId);
      setIsEditModalOpen(true);

      navigate(location.pathname, { replace: true });
    }
  }, [location.state]);

  const [selectedProductId, setSelectedProductId] = useState<string | null>(null);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const [isModalOpen, setIsModalOpen] = useState(false);  

  const [dataLowStock, setDataLowStock] = useState<ProductResponse[] | null>(null);
  const [errorLowStock, setErrorLowStock] = useState<string | null>(null);

  useEffect(() => {

    // Fetch dos produtos com estoque baixo
    fetchProductsFiltered(undefined, undefined, undefined)
      .then(data => {
        if (data.length === 0) {
          setDataLowStock([]);
        } else {
          setDataLowStock(data);
        }
      })
      .catch(err => {
        console.error("Erro ao buscar produtos com estoque baixo", err);
        setErrorLowStock("Não foi possível carregar os produtos com estoque baixo");
      });
  }, []);

  return (
    <div className="dashboard">
        <ProductModal
            isOpen={isModalOpen}
            onClose={() => setIsModalOpen(false)}
            onSuccess={() => {
                // opcional: recarregar lista depois
                fetchProductsFiltered().then(setDataLowStock);
            }}
        />
        <EditProductModal
          productId={selectedProductId}
          isOpen={isEditModalOpen}
          onClose={() => setIsEditModalOpen(false)}
          onSuccess={() => {
            fetchProductsFiltered().then(setDataLowStock);
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
                <h1>Produtos</h1>
                <button onClick={() => setIsModalOpen(true)}>
                    <img src={createIcon} alt="" />
                    <span>Criar</span>
                </button>
            </header>
            
            <div className="tabela-container">
              <table className="tabela">
                <thead className='header-container'>
                  <tr className='header-line'>
                    <th className="produto-id">Produto ID</th>
                    <th className="produto-nome">Produto</th>
                    <th className="produto-categoria">Categoria</th>
                    <th className="produto-estoque">Estoque</th>
                    <th className="produto-preco">Preço</th>
                  </tr>
                </thead>
                <tbody>
                  {errorLowStock && (
                    <tr className='linha-mensagem'>
                      <td colSpan={5} className="tabela-mensagem">{errorLowStock}</td>
                    </tr>
                  )}

                  {dataLowStock && dataLowStock.length === 0 && !errorLowStock && (
                    <tr className='linha-mensagem'>
                      <td colSpan={5} className="tabela-mensagem">Não existem produtos a serem listados</td>
                    </tr>
                  )}

                  {dataLowStock && dataLowStock.length > 0 && dataLowStock.map(produto => (
                    <tr
                      key={produto.id}
                      className='linha-produto'
                      onClick={() => {
                        setSelectedProductId(produto.id);
                        setIsEditModalOpen(true);
                      }}
                    >
                      <td className='id-td'>{produto.id}</td>
                      <td className='nome-td'>{produto.nome}</td>
                      <td className='categoria-td'>{produto.categoria.nome}</td>
                      <td className='quant-td'>{produto.quantidadeEmEstoque}</td>
                      <td className='preco-td'>R${produto.preco.toLocaleString()}</td>
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