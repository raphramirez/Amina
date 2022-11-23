
<br>

# Projects API

## Edit Project Request

```js
POST /projects/{id}
```

```json
{
  "name": "New Project 1",
  "description": "This is a an updated description.",
  "leadId": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
}
```

## Edit Project Response

```js
200 Ok
```

```json
{
    "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "name": "New Project 1",
    "description": "This is an updated description.",
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