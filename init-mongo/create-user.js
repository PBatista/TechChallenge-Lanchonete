db = db.getSiblingDB('lanchonete');

db.createUser({
  user: "admin",
  pwd: "admin123",
  roles: [
    { role: "readWrite", db: "lanchonete" },
    { role: "dbAdmin", db: "lanchonete" }
  ]
});
