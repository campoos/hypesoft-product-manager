import './Header.css';
import logo from "../../../assets/logo.png"
import search from "../../../assets/header/search.png"
import sun from "../../../assets/header/sun.png"
import notification from "../../../assets/header/notification.png"
import pfp from "../../../assets/header/pfp.png"

import { useState, useEffect } from 'react';

import { fetchProductsFiltered } from '../../../services/products.ts';
import type { ProductResponse } from '../../../services/products.ts';

export default function Header() {

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
                <li key={product.id}>{product.nome}</li>
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