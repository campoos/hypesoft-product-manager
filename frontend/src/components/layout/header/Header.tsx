import './Header.css';
import logo from "../../../assets/logo.png"
import search from "../../../assets/header/search.png"
import sun from "../../../assets/header/sun.png"
import notification from "../../../assets/header/notification.png"
import pfp from "../../../assets/header/pfp.png"
import arrow from "../../../assets/header/arrow.png"

import { useNavigate } from "react-router-dom";

import { useState, useEffect } from 'react';

import { fetchProductsFiltered } from '../../../services/products.ts';
import type { ProductResponse } from '../../../services/products.ts';

import keycloak, { type KeycloakTokenParsed } from '../../../auth/keycloak';

export default function Header() {
    const navigate = useNavigate();

        
    const [username, setUsername] = useState<string | null>(null);

    useEffect(() => {
        if (keycloak.authenticated) {
        const token: KeycloakTokenParsed | undefined = keycloak.tokenParsed;
        setUsername(token?.preferred_username || token?.name || null);
        }
    }, []);


    const handleLogout = () => {
        keycloak.logout({
            redirectUri: window.location.origin // volta para a página inicial após logout
        });
    };

    const [isDropdownOpen, setIsDropdownOpen] =useState(false)

    const toggleDropdown = () => {
        setIsDropdownOpen(prev => !prev)
    }

    const [query, setQuery] = useState('');
        const [results, setResults] = useState<ProductResponse[]>([]);

    useEffect(() => {
        if (query.length === 0) {
            setResults([]);
            return;
    }

    const delayDebounce = setTimeout(() => {
        fetchProductsFiltered(query).then(data => setResults(data));
    }, 300); // 300ms debounce

    return () => clearTimeout(delayDebounce);
    }, [query]);

  return (
    <header>
        <div className="logo-container">
            <div className="logo-img">
            <img src={logo} alt="Logo HypeProducts" />
            </div>
            <h1>Hype<strong>Products</strong></h1>
        </div>

        <form className="search-bar" role="search" onSubmit={e => e.preventDefault()}>
            <input
            type="text"
            placeholder="Pesquisar..."
            aria-label="Pesquisar produtos"
            value={query}
            onChange={e => setQuery(e.target.value)}
            />
            <button type="submit">
            <img src={search} alt="Buscar" />
            </button>

            {/* Lista de resultados */}
            {results.length > 0 && (
            <ul className="search-results">
                {results.map(product => (
                <li
                    key={product.id}
                    onClick={() => {
                        navigate("/produtos", {
                        state: { openProductId: product.id }
                        });
                        setResults([]);
                        setQuery('');
                    }}
                    >
                    {product.nome}
                </li>
                ))}
            </ul>
            )}
        </form>

        <nav className="header-actions" aria-label="Ações do usuário">
            <button type="button" aria-label="Alternar tema">
            <img src={sun} alt="" />
            </button>
            <button type="button" aria-label="Notificações">
            <img src={notification} alt="" />
            </button>

            <div className="separator"></div>

            <div className="profile" onClick={toggleDropdown}>
                <div className="profile-container">
                    <img src={pfp} alt="Profile picture" className='pfp' />
                    <div className="profile-data">
                        <h2>{username}</h2>
                        <span>Administrador</span>
                    </div>
                    <img src={arrow} className={`arrow ${isDropdownOpen ? 'up' : 'right'}`} alt="" />
                </div>
                {isDropdownOpen && (
                    <div className="dropdown">
                        <button onClick={handleLogout}>Sair</button>
                    </div>
                )}
            </div>
        </nav>
    </header>
  );
}