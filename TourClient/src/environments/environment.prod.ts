// proxy.conf.json forwards requests from localhost:4200 to my 
// webapi in azure https://tourwebapp2.azurewebsites.net/api/ for DEV

// proxy.conf.json forwards requests from https://tourangular.azurewebsites.net to my 
// webapi in azure https://tourwebapp2.azurewebsites.net/api/ for PROD

export const environment = {
  production: true,
  baseUrl: 'https://tourangular.azurewebsites.net/api'
};
