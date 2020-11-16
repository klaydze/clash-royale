# Clash Royale
Lousily developed and maintained by **Jessie Semana** using Angular 8 and ASP.Net Core 3.1 as backend (Web API).

## Development
#### Client
- Angular 8
- Bootstrap 4.x

#### Server
- ASP.Net Core 3.1
- Entity Framework Core

## Endpoints
| Route | Verb | Description |
| ------ | ------ | ------ |
| api/cards | Get | Get all card information |
| api/cards/{id} | Get | Get card information by id |
| api/arenas | Get | Get all arena information |
| api/arenas/{id} | Get | Get arena information by id |
| api/arenas/{id}/cards | Get | Get all cards that can be unlocked in the said arena |
| api/arenas/{id}/chests | Get | Get all chests that can be unlocked in the said arena |

### Additional...
In addition to the given endpoints, you can also do the following:
- Pagination
- Sorting
- Search

##### NOTE:
`api/cards` is the only endpoint that is currently support the mentioned above. I will update the other endpoint soon.

#### 1. Pagination

**Endpoint:**
`api/cards?[offset]=[0]&[limit]=[10]`

**Output:**
```
{
  "items": [],
  "totalSize": n
}
```

#### 2. Sorting

**Endpoint:**
`api/cards?orderBy=[fieldname] [asc|desc]`

**Output:**
```
{
  "items": [],
  "totalSize": n
}
```

#### 3. Search

**Endpoint:**
`api/cards?search=[fieldname] [operator] [value]`

**OR**

`api/cards?search=[fieldname] [operator] [value]&search=[fieldname] [operator] [value]`

**Output:**
```
{
  "items": [],
  "totalSize": n
}
```

**Valid and supported operators for searching and comparison**
- **eq** (Equals)
- **sw** (StartsWith)
- **co** (Contains)
- **lt** (LessThan)
- **lte** (LessThanEqual)
- **gt** (GreaterThan)
- **gte** (GreaterThanEqual)

## References
- [Building RESTful API with ION Specs](https://www.linkedin.com/learning/building-and-securing-restful-apis-in-asp-dot-net-core-2)

## Disclaimer
This content is not affiliated with, endorsed, sponsored, or specifically approved by Supercell and Supercell is not responsible for it. For more information see [Supercell’s Fan Content Policy](http://www.supercell.com/fan-content-policy).
