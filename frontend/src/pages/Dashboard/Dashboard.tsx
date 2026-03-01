import './Dashboard.css';
import { useEffect, useState } from 'react';
import { fetchDashboard } from '../../services/dashboards.ts';
import type { DashboardResponse } from '../../services/dashboards.ts';

import Header from '../../components/layout/header/Header.tsx'

import iconStockValue from "../../assets/cards/coins.png"
import iconTotalProducts from "../../assets/cards/boxes.png"
import iconLowStock from "../../assets/cards/warning.png"

export default function Dashboard() {

  const [dashboardData, setDashboardData] = useState<DashboardResponse | null>(null);

  useEffect(() => {
    fetchDashboard().then(data => setDashboardData(data));
  }, []);

  const cardsData = [
    { icon: iconStockValue, title: "Valor Total do Estoque", value: dashboardData?.valorTotalEstoque ? `R$${dashboardData.valorTotalEstoque.toLocaleString()}` : "..." },
    { icon: iconTotalProducts, title: "Total de Produtos", value: dashboardData?.totalProdutos ?? "..." },
    { icon: iconLowStock, title: "Produtos com Estoque Baixo", value: dashboardData?.produtosComEstoqueBaixo ?? "..." }
  ];

  return (
    <div className="dashboard">
      <Header></Header>
      <div className="body-container">
        <aside className="sidebar">
          <nav>
            <ul>
              <li><a href="/dashboard">Dashboard</a></li>
              <li><a href="/products">Products</a></li>
              <li><a href="/categories">Categories</a></li>
            </ul>
          </nav>
        </aside>
        <main className='main-content'>
          <h1>Dashboard</h1>
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
        </main>
      </div>
    </div>
  );
}