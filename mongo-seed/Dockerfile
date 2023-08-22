FROM mongo

COPY init.json /init.json
CMD mongoimport --host mongo-benefits --db employee-benefits --collection Employees --type json --file /init.json --jsonArray