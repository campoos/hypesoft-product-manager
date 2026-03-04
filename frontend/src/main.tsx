import React from 'react';
import ReactDOM from 'react-dom/client';
import AppRouter from './routes/AppRouter';
import AuthWrapper from './auth/AuthWrapper.tsx';
import './index.css';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <AuthWrapper>
      <AppRouter />
    </AuthWrapper>
  </React.StrictMode>
);