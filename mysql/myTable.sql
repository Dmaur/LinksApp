-- Set SQL modes and timezone
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

-- Drop existing tables if they exist
DROP TABLE IF EXISTS `tblLinks`;
DROP TABLE IF EXISTS `tblCategories`;
DROP TABLE IF EXISTS `tblUsers`;

-- Create Categories Table
CREATE TABLE IF NOT EXISTS `tblCategories` (
  `id` INT(10) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Create Links Table
CREATE TABLE IF NOT EXISTS `tblLinks` (
  `id` INT(10) NOT NULL AUTO_INCREMENT,
  `category_id` INT(10) NOT NULL,
  `label` VARCHAR(100) NOT NULL,
  `link` VARCHAR(200) NOT NULL,
  `pinned` BOOLEAN DEFAULT false,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`category_id`) REFERENCES `tblCategories`(`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Create Users Table
CREATE TABLE IF NOT EXISTS `tblUsers` (
  `id` INT(10) NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(50) NOT NULL UNIQUE,
  `password` VARCHAR(255) NOT NULL,
  `salted` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Insert Sample Data into Categories Table
INSERT INTO `tblCategories` (`name`) VALUES
('Play'),
('Learning'),
('Shopping'),
('Work');

-- Insert Sample Data into Links Table
INSERT INTO `tblLinks` (`category_id`, `label`, `link`, `pinned`) VALUES
(1, 'Netflix', 'https://www.netflix.com', true),
(1, 'Hulu', 'https://www.hulu.com', false),
(1, 'Disney', 'https://www.disneyplus.com', true);
-- (3, 'Amazon', 'https://www.amazon.com', true),
-- (3, 'eBay', 'https://www.ebay.com', false),
-- (3, 'Best Buy', 'https://www.bestbuy.com', false),
-- (1, 'Twitter', 'https://www.twitter.com', true),
-- (1, 'Facebook', 'https://www.facebook.com', false),
-- (1, 'Instagram', 'https://www.instagram.com', false),
-- (4, 'GitHub', 'https://www.github.com', false),
-- (2, 'Stack Overflow', 'https://www.stackoverflow.com', true),
-- (4, 'GitLab', 'https://www.gitlab.com', false),
-- (2, 'Google', 'https://www.google.com', true),
-- (2, 'Bing', 'https://www.bing.com', false),
-- (4, 'Bitbucket', 'https://www.bitbucket.org', false),
-- (4, 'DigitalOcean', 'https://www.digitalocean.com', false),
-- (3, 'Newegg', 'https://www.newegg.com', true),
-- (3, 'Walmart', 'https://www.walmart.com', false),
-- (4, 'LinkedIn', 'https://www.linkedin.com', false),
-- (1, 'Snapchat', 'https://www.snapchat.com', false),
-- (2, 'Reddit', 'https://www.reddit.com', true),
-- (4, 'Trello', 'https://www.trello.com', false);

-- Insert Data into Users pswrd is "salty" username "admin"
INSERT INTO `tblUsers` (`username`, `password`,`salted`) VALUES
('admin', 'x1mNM/9M271w+tfeZFwv2wb8PxhCDbrIvEtzc5/q2u4=','3IRtzAMmKloOkk4SbRTj+Q==');  
