
<br>

# Projects API

## Projects List Request

```js
GET /projects
```

## Projects List Response
```js
200 Ok
```

```json
[
  {
    "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "name": "Amina Attendance",
    "description": "Nisl nunc mi ipsum faucibus vitae aliquet.Lectus quam id leo in vitae turpis massa sed elementum.",
    "lead": {
      "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
      "name": "John Doe"
    }
  },
  {
    "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "name": "Cashjar",
    "description": "Condimentum non vulputate a, mattis et velit",
    "lead": {
      "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
      "name": "John Doe"
    }
  },
  {
    "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "name": "New Project",
    "description": "Etiam lectus nisi, condimentum non vulputate a, mattis et velit.",
    "lead": {
      "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
      "name": "John Doe"
    }
  }
]
```
