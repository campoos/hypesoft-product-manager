import './Sidebar.css';

import { Link } from "react-router-dom"

import dashboardicon from "../../../assets/sidebar/dashboards.png"
import produtosdicon from "../../../assets/sidebar/products.png"
import categoriasicon from "../../../assets/sidebar/category.png"
import configsicon from "../../../assets/sidebar/settings.png"

export default function Sidebar() {

  return (
    <aside className="sidebar">
        <nav>
            <ul>
                <span>GERAL</span>
                <Link to="/" className={`sidebarItem ${location.pathname === "/" ? "active" : ""}`}>
                    <img src={dashboardicon} alt="dashboardsIcon"/>
                    <span>Dashboards</span>
                </Link>

                <div className="category-separator"></div>

                <span>GERENCIAMENTO</span>
                <Link to="/produtos" className={`sidebarItem ${location.pathname === "/produtos" ? "active" : ""}`}>
                    <img src={produtosdicon} alt="produtosIcon"/>
                    <span>Produtos</span>
                </Link>
                
                <Link to="/categorias" className={`sidebarItem ${location.pathname === "/categorias" ? "active" : ""}`}>
                    <img src={categoriasicon} alt="categoriasIcon"/>
                    <span>Categorias</span>
                </Link>

                <div className="separator"></div>

                <span>SUPORTE</span>
                <Link 
                    to="#"
                    className="sidebarItem disabled"
                    onClick={(e) => e.preventDefault()}
                    title="Em desenvolvimento"
                >
                    <img src={configsicon} alt="configuracoesIcon"/>
                    <span>Configurações</span>
                </Link>
            </ul>
        </nav>
    </aside>
  );
}