// src/pages/dashboard/Dashboard.tsx
import './Dashboard.css';
import { useEffect, useState } from 'react';

import { useDashboardData } from '../../hooks/useDashboardData';
import { fetchProductsFiltered } from '../../services/products';
import type { ProductResponse } from '../../services/products.ts';

import Header from '../../components/layout/header/Header';
import Sidebar from '../../components/layout/sidebar/Sidebar';

import iconStockValue from "../../assets/cards/coins.png";
import iconTotalProducts from "../../assets/cards/boxes.png";
import iconLowStock from "../../assets/cards/warning.png";
import iconLowStockList from "../../assets/cards/low-stock.png";

export default function Dashboard() {

  const estoqueMax = 9;

  // Hook que já retorna dashboardData, loading e error
  const { dashboardData, loading, error } = useDashboardData();

  // Estado para produtos com estoque baixo
  const [dataLowStock, setDataLowStock] = useState<ProductResponse[] | null>(null);
  const [errorLowStock, setErrorLowStock] = useState<string | null>(null);

  useEffect(() => {
    fetchProductsFiltered(undefined, undefined, estoqueMax)
      .then(data => {
        if (data.length === 0) setDataLowStock([]);
        else setDataLowStock(data);
      })
      .catch(err => {
        console.error("Erro ao buscar produtos com estoque baixo", err);
        setErrorLowStock("Não foi possível carregar os produtos com estoque baixo");
      });
  }, []);

  const cardsData = [
    { icon: iconStockValue, title: "Valor Total do Estoque", value: dashboardData?.valorTotalEstoque ? `R$${dashboardData.valorTotalEstoque.toLocaleString()}` : "..." },
    { icon: iconTotalProducts, title: "Total de Produtos", value: dashboardData?.totalProdutos ?? "..." },
    { icon: iconLowStock, title: "Produtos com Estoque Baixo", value: dashboardData?.produtosComEstoqueBaixo ?? "..." }
  ];

  if (loading) return <p>Carregando dashboard...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="dashboard">
      <Header />
      <div className="body-container">
        <Sidebar />
        <main className='main-container'>
          <div className="main-content">
            <h1>Dashboard</h1>

            {/* Cards */}
            <section className="cards-container">
              {cardsData.map((card, index) => (
                <article className="card" key={index}>
                  <div className="card-header">
                    <div className="image-container">
                      <img src={card.icon} alt={card.title} />
                    </div>
                    <h2>{card.title}</h2>
                  </div>
                  <p className='card-value'>{card.value}</p>
                </article>
              ))}
            </section>

            {/* Tabela de produtos com estoque baixo */}
            <div className="tabela-container">
              <div className="tabela-section-header">
                <div className="container-image">
                  <img src={iconLowStockList} alt="" />
                </div>
                <h2>Produtos com estoque baixo</h2>
                <h3>Estoque ≤ {estoqueMax} unidades</h3>
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
                      <td colSpan={5} className="tabela-mensagem">Não existem produtos em baixo estoque</td>
                    </tr>
                  )}

                  {dataLowStock && dataLowStock.length > 0 && dataLowStock.map(produto => (
                    <tr key={produto.id} className='linha-produto'>
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