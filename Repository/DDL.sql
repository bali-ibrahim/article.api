CREATE SCHEMA utility CHAR SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE utility;
DELIMITER $$
CREATE FUNCTION UPPER_TR(str TEXT) RETURNS TEXT
BEGIN
    RETURN UPPER(REPLACE(str, 'i', 'İ'));
END$$
DELIMITER ;

DELIMITER $$
CREATE FUNCTION LOWER_TR(str TEXT) RETURNS TEXT
BEGIN
    RETURN LOWER(REPLACE(str, 'I', 'ı'));
END$$
DELIMITER ;

CREATE SCHEMA article CHAR SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE article;
DROP TABLE IF EXISTS meta;
CREATE TABLE `meta` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `Title` varchar(64) COLLATE utf8mb4_unicode_ci NOT NULL,
  `AuthorName` varchar(64) COLLATE utf8mb4_unicode_ci NOT NULL,
  `LastEditedTimestamp` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `_DB_META_CREATEDTIMESTAMP` timestamp NOT NULL DEFAULT current_timestamp(),
  `_DB_META_MODIFIEDTIMESTAMP` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `idx1` (`Title`) USING BTREE,
  KEY `idx2` (`Title`),
  FULLTEXT KEY `FTK_TitleAuthorName` (`Title`,`AuthorName`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
;

DROP TABLE IF EXISTS context;
CREATE TABLE `context` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `MetaId` bigint(20) unsigned NOT NULL,
  `Body` longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `_DB_META_CREATEDTIMESTAMP` timestamp NOT NULL DEFAULT current_timestamp(),
  `_DB_META_MODIFIEDTIMESTAMP` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `context_meta_Id_fk` (`MetaId`),
  FULLTEXT KEY `FTK_Body` (`Body`),
  CONSTRAINT `context_meta_Id_fk` FOREIGN KEY (`MetaId`) REFERENCES `meta` (`Id`)
)
;


DROP PROCEDURE IF EXISTS sp_search_meta;
DELIMITER $$
CREATE PROCEDURE sp_search_meta(IN var_pattern TEXT)
BEGIN
    DECLARE var_char_friendly_pattern TEXT
        DEFAULT CONCAT(utility.UPPER_TR(var_pattern), ' ', utility.LOWER_TR(var_pattern));

    -- enforce single field data type while still being agnostic to the data type
    DROP TEMPORARY TABLE IF EXISTS tmp_match;
    CREATE TEMPORARY TABLE tmp_match
    SELECT Id AS MetaId FROM meta LIMIT 0;

    -- if its a full word
    INSERT INTO tmp_match
    SELECT Id
    FROM meta
    WHERE MATCH(Title, AuthorName) AGAINST(var_char_friendly_pattern)
    ;

    -- if not full word

    -- why: can't reopen a temp table
    DROP TEMPORARY TABLE IF EXISTS tmp_match_2;
    CREATE TEMPORARY TABLE tmp_match_2 LIKE tmp_match;
    INSERT INTO tmp_match_2 SELECT * FROM tmp_match;

    INSERT INTO tmp_match
    SELECT m.Id
    FROM meta m
    LEFT JOIN tmp_match_2 t ON m.Id = t.MetaId
    WHERE TRUE
        -- don't look for already included
        AND t.MetaId IS NULL
        AND m.Title REGEXP var_pattern
    ;

    -- why: can't reopen a temp table
    DROP TEMPORARY TABLE IF EXISTS tmp_match_2;
    CREATE TEMPORARY TABLE tmp_match_2 LIKE tmp_match;
    INSERT INTO tmp_match_2 SELECT * FROM tmp_match;

    INSERT INTO tmp_match
    SELECT m.Id
    FROM meta m
    LEFT JOIN tmp_match_2 t ON m.Id = t.MetaId
    WHERE TRUE
        -- don't look for already included
        AND t.MetaId IS NULL
        AND m.AuthorName REGEXP var_pattern
    ;

    SELECT m.*
    FROM meta m
    JOIN tmp_match tm ON m.Id = tm.MetaId
    ;


END$$

DELIMITER ;

DROP PROCEDURE IF EXISTS sp_search_context;
DELIMITER $$
CREATE PROCEDURE sp_search_context(IN var_pattern TEXT)
BEGIN
    -- TODO: DRY
    DECLARE var_char_friendly_pattern TEXT
        DEFAULT CONCAT(utility.UPPER_TR(var_pattern), ' ', utility.LOWER_TR(var_pattern));

    -- enforce single field data type while still being agnostic to the data type
    DROP TEMPORARY TABLE IF EXISTS tmp_match;
    CREATE TEMPORARY TABLE tmp_match
    SELECT Id AS MetaId FROM meta LIMIT 0;

    -- if its a full word
    INSERT INTO tmp_match
    SELECT MetaId
    FROM context
    WHERE MATCH(Body) AGAINST(var_char_friendly_pattern)
    ;

    -- if not full word

    -- why: can't reopen a temp table
    DROP TEMPORARY TABLE IF EXISTS tmp_match_2;
    CREATE TEMPORARY TABLE tmp_match_2 LIKE tmp_match;
    INSERT INTO tmp_match_2 SELECT * FROM tmp_match;

    INSERT INTO tmp_match
    SELECT c.MetaId
    FROM context c
    LEFT JOIN tmp_match_2 t ON c.MetaId = t.MetaId
    WHERE TRUE
        -- don't look for already included
        AND t.MetaId IS NULL
        AND c.Body REGEXP var_pattern
    ;

    SELECT m.*
    FROM meta m
    JOIN tmp_match tm ON m.Id = tm.MetaId
    ;


END$$

DELIMITER ;

#
# INSERT INTO meta (Title, AuthorName, LastEditedTimestamp)
# VALUES ('çöküş ağı', 'İ. Bali', CURRENT_TIMESTAMP)
# ;
#
#
# SET @var_str := 'bali';
# # EXPLAIN
# SELECT *
# FROM meta m
# WHERE MATCH(Title, AuthorName) AGAINST(@var_ag)
# ;
#
# CALL sp_search_meta(@var_str);

# SHOW CREATE TABLE context;