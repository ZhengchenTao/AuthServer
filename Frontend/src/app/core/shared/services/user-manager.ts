import {
  MetadataServiceCtor,
  OidcClientSettings,
  ResponseValidator,
  SigninResponse,
  SignoutResponse,
  UserManagerSettings,
  WebStorageStateStore
} from 'oidc-client';
// @ts-ignore
import { JoseUtil } from 'oidc-client/src/JoseUtil.js';
// @ts-ignore
import { Log } from 'oidc-client/src/Log.js';
// @ts-ignore
import { ResponseValidator as ResponseValidatorSrc } from 'oidc-client/src/ResponseValidator';
import { createUserManager } from 'redux-oidc';

function validateJwtAttributes(
  settings: OidcClientSettings,
  jwt: string,
  issuer: string,
  audience: string,
  clockSkew: number,
  now: number,
  timeInsensitive: boolean
): Promise<any> {
  if (!clockSkew) {
    clockSkew = 0;
  }

  if (!now) {
    now = Date.now() / 1000;
  }

  // @ts-ignore
  const payload = this.parseJwt(jwt).payload;

  if (!payload.iss) {
    Log.error('JoseUtil._validateJwt: issuer was not provided');
    return Promise.reject(new Error('issuer was not provided'));
  }
  let issuerNotMatch = true;
  for (const key of settings.signingKeys!) {
    if (
      payload.iss === key.issuer ||
      (key.issuer.includes('{tenantid}') && payload.iss === key.issuer.replace('{tenantid}', payload.tid))
    ) {
      issuerNotMatch = false;
      break;
    }
  }
  if (issuerNotMatch) {
    Log.error('JoseUtil._validateJwt: Invalid issuer in token', payload.iss);
    return Promise.reject(new Error('Invalid issuer in token: ' + payload.iss));
  }

  if (!payload.aud) {
    Log.error('JoseUtil._validateJwt: aud was not provided');
    return Promise.reject(new Error('aud was not provided'));
  }
  const validAudience = payload.aud === audience || (Array.isArray(payload.aud) && payload.aud.indexOf(audience) >= 0);
  if (!validAudience) {
    Log.error('JoseUtil._validateJwt: Invalid audience in token', payload.aud);
    return Promise.reject(new Error('Invalid audience in token: ' + payload.aud));
  }
  if (payload.azp && payload.azp !== audience) {
    Log.error('JoseUtil._validateJwt: Invalid azp in token', payload.azp);
    return Promise.reject(new Error('Invalid azp in token: ' + payload.azp));
  }

  if (!timeInsensitive) {
    const lowerNow = now + clockSkew;
    const upperNow = now - clockSkew;

    if (!payload.iat) {
      Log.error('JoseUtil._validateJwt: iat was not provided');
      return Promise.reject(new Error('iat was not provided'));
    }
    if (lowerNow < payload.iat) {
      Log.error('JoseUtil._validateJwt: iat is in the future', payload.iat);
      return Promise.reject(new Error('iat is in the future: ' + payload.iat));
    }

    if (payload.nbf && lowerNow < payload.nbf) {
      Log.error('JoseUtil._validateJwt: nbf is in the future', payload.nbf);
      return Promise.reject(new Error('nbf is in the future: ' + payload.nbf));
    }

    if (!payload.exp) {
      Log.error('JoseUtil._validateJwt: exp was not provided');
      return Promise.reject(new Error('exp was not provided'));
    }
    if (payload.exp < upperNow) {
      Log.error('JoseUtil._validateJwt: exp is in the past', payload.exp);
      return Promise.reject(new Error('exp is in the past:' + payload.exp));
    }
  }

  return Promise.resolve(payload);
}

class ResponseValidatorMsal extends ResponseValidatorSrc {
  constructor(settings: OidcClientSettings) {
    super(settings);
    (this as any)._joseUtil.validateJwtAttributes = validateJwtAttributes.bind(JoseUtil, settings);
  }

  validateSigninResponse(state: any, response: any): Promise<SigninResponse> {
    return super.validateSigninResponse(state, response);
  }

  validateSignoutResponse(state: any, response: any): Promise<SignoutResponse> {
    return super.validateSignoutResponse(state, response);
  }
}

function ResponseValidatorCtor(
  settings: OidcClientSettings,
  metadataServiceCtor?: MetadataServiceCtor,
  userInfoServiceCtor?: any
): ResponseValidator {
  return new ResponseValidatorMsal(settings);
}

const userManagerConfig: UserManagerSettings = {
  client_id: '1d3702d0-c055-4a04-98ec-f3a35e1ec92b',
  redirect_uri: window.location.origin + '/login',
  post_logout_redirect_uri: window.location.origin + '/',
  response_type: 'token id_token',
  scope: 'openid profile email user.read',
  authority: 'https://login.microsoftonline.com/common/v2.0',
  silent_redirect_uri: window.location.origin + '/silent-refresh.html',
  automaticSilentRenew: true,
  // accessTokenExpiringNotificationTime: 3590, // Debug only.
  filterProtocolClaims: true,
  loadUserInfo: true,
  userStore: new WebStorageStateStore({ store: localStorage }),
  ResponseValidatorCtor
};

export const localStorageKeyOfLastVisited = 'urlToGo';
export const userManager = createUserManager(userManagerConfig);
