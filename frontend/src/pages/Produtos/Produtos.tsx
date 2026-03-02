import './Produtos.css';
import { useEffect, useState } from 'react';

import { fetchProductsFiltered } from '../../services/products.ts';
import type { ProductResponse } from '../../services/products.ts';
import { fetchCategorias } from "../../services/categories.ts";
import type { CategoriaResponse } from "../../services/categories";

import Header from '../../components/layout/header/Header.tsx'
import Sidebar from '../../components/layout/sidebar/Sidebar.tsx'
import EditProductModal from '../../components/forms/editProduct/EditProductModal.tsx';
import ProductModal from '../../components/forms/product/ProductModal.tsx';

import { useLocation, useNavigate } from "react-router-dom";

import createIcon from "../../assets/plus.png"

export default function Produtos() {

  const [filters, setFilters] = useState({
    nome: "",
    categoriaId: "",
    estoqueMax: ""
  });

  const [categorias, setCategorias] = useState<CategoriaResponse[]>([]);

  useEffect(() => {
    fetchCategorias().then(setCategorias);
  }, []);

  useEffect(() => {
    const delay = setTimeout(() => {
      fetchProductsFiltered(
        filters.nome || undefined,
        filters.categoriaId || undefined,
        filters.estoqueMax ? Number(filters.estoqueMax) : undefined
      )
        .then(setDataLowStock)
        .catch(() => setErrorLowStock("Erro ao buscar produtos"));
    }, 300); // debounce

    return () => clearTimeout(delay);
  }, [filters]);

  const handleFilterChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;

    setFilters(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    if (location.state?.openProductId) {
      setSelectedProductId(location.state.openProductId);
      setIsEditModalOpen(true);

      navigate(location.pathname, { replace: true });
    }
  }, [location.state, navigate]);

  const [selectedProductId, setSelectedProductId] = useState<string | null>(null);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const [isModalOpen, setIsModalOpen] = useState(false);  

  const [dataLowStock, setDataLowStock] = useState<ProductResponse[]>([]);
  const [errorLowStock, setErrorLowStock] = useState<string | null>(null);

  return (
    <div className="dashboard">
        <ProductModal
            isOpen={isModalOpen}
            onClose={() => setIsModalOpen(false)}
            onSuccess={() => {
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
              <div className="filters">
                <input
                  type="text"
                  name="nome"
                  placeholder="Buscar por nome..."
                  value={filters.nome}
                  onChange={handleFilterChange}
                />

                <select
                  name="categoriaId"
                  value={filters.categoriaId}
                  onChange={handleFilterChange}
                >
                  <option value="">Todas categorias</option>

                  {categorias.map((cat) => (
                    <option key={cat.id} value={cat.id}>
                      {cat.nome}
                    </option>
                  ))}
                </select>

                <input
                  type="number"
                  name="estoqueMax"
                  placeholder="Estoque máximo"
                  value={filters.estoqueMax}
                  onChange={handleFilterChange}
                />
              </div>
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