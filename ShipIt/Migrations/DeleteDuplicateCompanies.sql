-- step 1
CREATE TABLE gcp_temp (LIKE gcp);

-- step 2
INSERT INTO gcp_temp(gcp_cd, gln_nm, gln_addr_02, gln_addr_03, gln_addr_04, gln_addr_postalcode, gln_addr_city, contact_tel, contact_mail)
SELECT
    DISTINCT ON (gcp_cd) gcp_cd, gln_nm, gln_addr_02, gln_addr_03, gln_addr_04, gln_addr_postalcode, gln_addr_city, contact_tel, contact_mail
FROM gcp;

-- step 3
DROP TABLE gcp;

-- step 4
ALTER TABLE gcp_temp
    RENAME TO gcp; 