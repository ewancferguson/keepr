CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

ALTER TABLE accounts
ADD cover_img VARCHAR(1000) 

CREATE TABLE keeps (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  views INT UNSIGNED NOT NULL DEFAULT 0,
  creator_id VARCHAR(255) NOT NULL, 
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

DROP TABLE keeps

CREATE TABLE vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000),
  is_private BOOLEAN NOT NULL,
  creator_id VARCHAR(225) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

DROP TABLE vaults





CREATE TABLE vault_keeps (
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  keep_id int NOT NULL,
  vault_id int NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (keep_id) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (vault_id) REFERENCES vaults(id) ON DELETE CASCADE
)






DROP TABLE vault_keeps



    SELECT 
    vault_keeps.*,
    keeps.*,
    vaults.*
    FROM vault_keeps
    JOIN keeps ON vault_keeps.keep_id = keeps.id
    JOIN vaults ON vault_keeps.vault_id = vaults.id
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE vault_keeps.vault_id = 31


    


SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON keeps.creator_id = accounts.id
    WHERE creator_id = '679017bf9406e615cee24f9c';


 SELECT
      keeps.*,
      COUNT(vault_keeps.id) AS kept,
      accounts.*
      FROM keeps
      JOIN accounts ON keeps.creator_id = accounts.id
      LEFT JOIN vault_keeps ON keeps.id = vault_keeps.keep_id
      WHERE keeps.id = 28