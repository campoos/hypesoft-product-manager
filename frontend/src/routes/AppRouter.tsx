import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Dashboard from '../pages/Dashboard/Dashboard.tsx';
import Produtos from '../pages/Produtos/Produtos.tsx';
import Categorias from '../pages/Categorias/Categorias.tsx';
import Configuracoes from '../pages/Configuracoes/Configuracoes.tsx';

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/produtos" element={<Produtos />} />
        <Route path="/categorias" element={<Categorias />} />
        <Route path="/configuracoes" element={<Configuracoes />} />
      </Routes>
    </BrowserRouter>
  );
}