import './Dashboard.css';
import Header from '../../components/layout/header/Header.tsx'

import iconStockValue from "../../assets/cards/coins.png"
import iconTotalProducts from "../../assets/cards/boxes.png"
import iconLowStock from "../../assets/cards/warning.png"

export default function Dashboard() {

  const cardsData = [
    { icon: iconStockValue, title: "Valor Total do Estoque", value: "R$2.500,00" },
    { icon: iconTotalProducts, title: "Total de Produtos", value: 350 },
    { icon: iconLowStock, title: "Produtos com Estoque Baixo", value: 12 }
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
          <div className="cards-container">
            {cardsData.map((card, index) => (
              <div className="card" key={index}>
                <div className="card-header">
                  <div className="image-container">
                    <img src={card.icon} alt={card.title} />
                  </div>
                  <h2>{card.title}</h2>
                </div>
                <p>{card.value}</p>
              </div>
            ))}
          </div>
        </main>
      </div>
    </div>
  );
}