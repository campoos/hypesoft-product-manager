import './Header.css';
import logo from "../../../assets/logo.png"
import search from "../../../assets/header/search.png"
import sun from "../../../assets/header/sun.png"
import notification from "../../../assets/header/notification.png"
import pfp from "../../../assets/header/pfp.png"

export default function Header() {
  return (
    <header>
        <div className="logo-container">
            <div className="logo-img">
            <img src={logo} alt="Logo HypeProducts" />
            </div>
            <h1>Hype<strong>Products</strong></h1>
        </div>

        <form className="search-bar" role="search">
            <input type="text" placeholder="Pesquisar..." aria-label="Pesquisar produtos" />
            <button type="submit">
            <img src={search} alt="Buscar" />
            </button>
        </form>

        <nav className="header-actions" aria-label="Ações do usuário">
            <button type="button" aria-label="Alternar tema">
            <img src={sun} alt="" />
            </button>
            <button type="button" aria-label="Notificações">
            <img src={notification} alt="" />
            </button>

            <div className="separator"></div>

            <div className="profile">
            <img src={pfp} alt="Profile picture" />
            <div className="profile-data">
                <h2>Henrique Araujo</h2>
                <span>Administrador</span>
            </div>
            </div>
        </nav>
    </header>
  );
}