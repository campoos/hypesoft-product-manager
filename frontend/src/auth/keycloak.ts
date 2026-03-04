import Keycloak from 'keycloak-js';
import type { KeycloakTokenParsed } from './keycloakTypes';

const keycloak: any = new Keycloak({
  url: 'http://localhost:8080', // URL do seu Keycloak local
  realm: 'hypesoft',      // Nome do Realm que você criou
  clientId: 'meu-site',         // Client ID que você criou
});

export type { KeycloakTokenParsed };
export default keycloak;