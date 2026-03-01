import './Sidebar.css';

export default function Sidebar() {

  return (
    <aside className="sidebar">
        <nav>
            <ul>
                <span>GERAL</span>
                <li><a href="/">Dashboard</a></li>
                <span>GERENCIAMENTO</span>
                <li><a href="/produtos">Produtos</a></li>
                <li><a href="/categorias">Categorias</a></li>

                <div className="separator"></div>

                <span>SUPORTE</span>
                <li><a href="/configuracoes">Configurações</a></li>
            </ul>
        </nav>
    </aside>
  );
}