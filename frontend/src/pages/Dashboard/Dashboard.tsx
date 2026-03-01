import './Dashboard.css';
import Header from '../../components/layout/header/Header.tsx'

export default function Dashboard() {
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
          <h1>main content aqui</h1>
        </main>
      </div>
    </div>
  );
}