import Keycloak from 'keycloak-js';

const keycloak = new Keycloak({
  url: 'http://localhost:8080',
  realm: 'realm-keycloak',
  clientId: 'meu-site',
});

export default keycloak;