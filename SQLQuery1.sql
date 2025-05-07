CREATE TABLE Asset (
    AssetID INT PRIMARY KEY,
    AssetName VARCHAR(255) NOT NULL,
    ValueType VARCHAR(20) NOT NULL,
    Value DECIMAL(18, 2) NOT NULL,
    PurchaseDate DATE,
    Description TEXT,
    Category VARCHAR(50)
);