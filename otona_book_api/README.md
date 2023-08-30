# Useful commands

## generate models
```bash
$ dotnet ef dbcontext scaffold \
  "Host=localhost;Port=5455;Database=otona_book;Username=zcxb;Password=123456" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  -o ./DataAccess/Models \
  --no-build \
  --json \
  -f \
  --prefix-output \
  --no-pluralize \
  --data-annotations
```
