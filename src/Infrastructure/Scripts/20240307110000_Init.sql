CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Cocktail" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Cocktail" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "CreatedOn" TEXT NOT NULL,
    "LastModifiedOn" TEXT NULL,
    "DeletedOn" TEXT NULL
);

CREATE TABLE "Ingredient" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Ingredient" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "AlcoholLevel" REAL NOT NULL,
    "CreatedOn" TEXT NOT NULL,
    "LastModifiedOn" TEXT NULL,
    "DeletedOn" TEXT NULL
);

CREATE TABLE "Step" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Step" PRIMARY KEY,
    "Order" INTEGER NOT NULL,
    "Description" TEXT NOT NULL,
    "CocktailId" TEXT NULL,
    "CreatedOn" TEXT NOT NULL,
    "LastModifiedOn" TEXT NULL,
    "DeletedOn" TEXT NULL,
    CONSTRAINT "FK_Step_Cocktail_CocktailId" FOREIGN KEY ("CocktailId") REFERENCES "Cocktail" ("Id")
);

CREATE TABLE "Composition" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Composition" PRIMARY KEY,
    "Quantity" REAL NOT NULL,
    "Unit" INTEGER NOT NULL,
    "IngredientId" TEXT NOT NULL,
    "CocktailId" TEXT NULL,
    "CreatedOn" TEXT NOT NULL,
    "LastModifiedOn" TEXT NULL,
    "DeletedOn" TEXT NULL,
    CONSTRAINT "FK_Composition_Cocktail_CocktailId" FOREIGN KEY ("CocktailId") REFERENCES "Cocktail" ("Id"),
    CONSTRAINT "FK_Composition_Ingredient_IngredientId" FOREIGN KEY ("IngredientId") REFERENCES "Ingredient" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Composition_CocktailId" ON "Composition" ("CocktailId");

CREATE INDEX "IX_Composition_IngredientId" ON "Composition" ("IngredientId");

CREATE INDEX "IX_Step_CocktailId" ON "Step" ("CocktailId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240307110000_Init', '8.0.2');

COMMIT;

