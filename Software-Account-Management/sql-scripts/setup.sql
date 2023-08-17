CREATE TABLE APP_LICENSES (
  id INT NOT NULL PRIMARY KEY,
  app_name VARCHAR(10) NOT NULL,
  app_service VARCHAR(10) NOT NULL,
  space_id INT(10) NOT NULL,
  test_station_pool VARCHAR(10) NOT NULL,
  user_name VARCHAR NOT NULL,
  password VARCHAR NOT NULL,
  reservation INT(10),
  last_modified DATETIME NOT NULL,
  license_status BOOLEAN(10) NOT NULL,
  queue NCHAR(10) NOT NULL,
  FOREIGN KEY (reservation) REFERENCES LICENSE_ORDER_BOOK(id),
  FOREIGN KEY (queue) REFERENCES LICENSE_QUEUE(app_id)
);

CREATE TABLE LICENSE_ORDER_BOOK (
  id INT NOT NULL PRIMARY KEY,
  app_id NCHAR(10) NOT NULL,
  test_station NCHAR(10) NOT NULL,
  order_time NCHAR(10) NOT NULL,
  reservation_time NCHAR(10) NOT NULL,
  completion_time NCHAR(10) NOT NULL,
  order_status NCHAR(10) NOT NULL,
  reservedbyuser NCHAR(10) NOT NULL,
  reservedforsut NCHAR(10) NOT NULL,
  instanceid NCHAR(10) NOT NULL,
  teststationtaskid NCHAR(10) NOT NULL
);

CREATE TABLE LICENSE_QUEUE (
  id INT NOT NULL PRIMARY KEY,
  app_id NCHAR(10) NOT NULL,
  order_id INT NOT NULL,
  FOREIGN KEY (app_id) REFERENCES APP_LICENSES(queue),
  FOREIGN KEY (order_id) REFERENCES LICENSE_ORDER_BOOK(id)
);