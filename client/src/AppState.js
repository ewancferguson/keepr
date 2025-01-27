import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  keeps: [],
  /** @type {import('./models/Keep.js').Keep} user info from the database*/
  activeKeep: null,
  /** @type {import('./models/Vault.js').Vault[]} user info from the database*/
  myVaults: [],
})

