Events =
  trigger: (actions) -> action() for action in @[actions]
  nextTurn: []

module.exports = Events
