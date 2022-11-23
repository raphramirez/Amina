
<br>

# Projects API

## Get Project By ID Request

```js
GET /projects/{id}
```


## Get Project By ID Response

```js
200 Ok
```

```json
{
    "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "name": "Amina Attendance",
    "description": "Nisl nunc mi ipsum faucibus vitae aliquet.Lectus quam id leo in vitae turpis massa sed elementum.",
    "lead": {
      "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
      "name": "John Doe",
      "status": "ClockedIn"
    },
    "members": [
        {
          "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
          "name": "Georg Guy",
          "status": "ClockedIn"
        },
        {
          "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
          "name": "Donata Ana",
          "status": "ClockedIn"
        },
        {
          "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
          "name": "Raphael Ramirez",
          "status": "OnLeave"
        }
    ]
}
```