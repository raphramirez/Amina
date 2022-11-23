
<br>

# Projects API

## Create Project Request

```js
POST /projects
```

```json
{
  "name": "New Project 1",
  "description": "This is a description.",
  "leadId": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
  "memberIds": [
    "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
  ]
}
```

## Create Project Response

```js
201 Created
```

```json
{
    "id": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "name": "New Project 1",
    "description": "This is a description.",
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