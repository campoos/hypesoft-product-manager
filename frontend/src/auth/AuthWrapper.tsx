import React, { useEffect, useState } from 'react';
import keycloak from './keycloak'; // caminho da instância Keycloak

interface Props {
  children: React.ReactNode;
}

const AuthWrapper: React.FC<Props> = ({ children }) => {
  const [authenticated, setAuthenticated] = useState(false);

  useEffect(() => {
    // Inicializa o Keycloak diretamente
    keycloak
      .init({ onLoad: 'login-required' })
      .then((auth: boolean) => setAuthenticated(auth))
      .catch(() => setAuthenticated(false));
  }, []);

  if (!authenticated) return <div>Loading Keycloak...</div>;

  return <>{children}</>;
};

export default AuthWrapper;