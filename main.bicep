param location string = 'westus3'
param storageAccountName string = 'toylaunch${uniqueString(resourceGroup().id)}'
param appServiceAppName string = 'toylaunch${uniqueString(resourceGroup().id)}'

// var appServicePlanName = 'toy-product-launch-plan'

var storageAccountSkuName = (environmentType == 'prod') ? 'Standard_GRS' : 'Standard_LRS'
// var appServicePlanSkuName = (environmentType == 'prod') ? 'P2_v3' : 'F1'

@allowed([
  'nonprod'
  'prod'
])
param environmentType string

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-08-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: storageAccountSkuName
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
  }
}

module appService './appService.bicep' = {
  name: 'appService'
  params: {
    location: location
    appServiceAppName: appServiceAppName
    environmentType: environmentType
  }
}

// resource appServicePlan 'Microsoft.Web/serverFarms@2021-03-01' = {
//   name: appServiceAppName
//   location: location
//   sku: {
//     name: appServicePlanSkuName
//   }
// }

// resource appServiceApp 'Microsoft.Web/sites@2021-03-01' = {
//   name: appServicePlanName
//   location: location
//   properties: {
//     serverFarmId: appServicePlan.id
//     httpsOnly: true
//   }
// }

output appServiceAppHostName string = appService.outputs.appServiceAppHostName
