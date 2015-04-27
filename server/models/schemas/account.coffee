mongoose = require 'mongoose'

accountSchema = mongoose.Schema
  username: String
  password: String
  friends: [String]

Account = mongoose.model 'Account', accountSchema

module.exports = Account
