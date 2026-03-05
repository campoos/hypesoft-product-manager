import Keycloak from 'keycloak-js';
import type { KeycloakTokenParsed } from './keycloakTypes';

const keycloak: any = new Keycloak({
  url: 'http://localhost:8080', // URL do Keycloak local
  realm: 'hypesoft',      // Nome do Realm
  clientId: 'meu-site',         // Client ID
});

export type { KeycloakTokenParsed };
export default keycloak;