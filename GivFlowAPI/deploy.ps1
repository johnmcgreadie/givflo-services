az webapp up --resource-group rg-dev-givflo-api --sku F1 --name dev-givflo-api --location australiaeast

az webapp config appsettings set  --name dev-givflo-api  --resource-group rg-dev-givflo-api --settings ASPNETCORE_ENVIRONMENT="Development"