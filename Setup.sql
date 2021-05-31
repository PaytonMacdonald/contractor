-- CREATE TABLE contractors (
--   id INT primary key NOT NULL AUTO_INCREMENT,
--   title VARCHAR(255) NOT NULL,
--   body VARCHAR(255) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL,
--   FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
-- );
-- CREATE TABLE jobs (
--   id INT primary key NOT NULL AUTO_INCREMENT,
--   title VARCHAR(255) NOT NULL,
--   body VARCHAR(255) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL,
--   FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
-- );
-- CREATE TABLE jobs_contractors (
--   id INT primary key NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   jobId INT NOT NULL,
--   contractorId INT NOT NULL,
--   FOREIGN KEY (creatorId) REFERENCES accounts (id),
--   FOREIGN KEY (jobId) REFERENCES jobs (id),
--   FOREIGN KEY (contractorId) REFERENCES contractors (id) ON DELETE CASCADE
-- )
/* !!!! DANGER ZONE !!!! */
/* REMOVE ALL DATA FROM TABLE */
-- DELETE FROM artists;
/* DELETE ENTIRE TABLE */
-- DROP TABLE profiles
-- DROP TABLE blogs
-- DROP TABLE comments
/*////////////////////////*/