export interface KeycloakTokenParsed {
  preferred_username?: string;
  email?: string;
  name?: string;

  realm_access?: {
    roles?: string[];
  }
}