import './Categorias.css';

import Header from '../../components/layout/header/Header.tsx'
import Sidebar from '../../components/layout/sidebar/Sidebar.tsx'

export default function Categorias() {
  return (
    <div className="dashboard">
      <Header></Header>
      <div className="body-container">

        <Sidebar></Sidebar>

        <main className='main-container'>
          <div className="main-content">
            <h1>Categorias</h1>
           
          </div>
        </main>
      </div>
    </div>
  );
}