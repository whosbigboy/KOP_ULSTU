
-- Удаляем старые таблицы если есть
DROP TABLE IF EXISTS employees, employee_orgs;

-- Создаем подразделения с именем которое ищет код
CREATE TABLE employee_orgs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) NOT NULL UNIQUE
);

-- Создаем сотрудников
CREATE TABLE employees (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    fio VARCHAR(200) NOT NULL,
    posts VARCHAR(500),
    employee_org_id UUID NOT NULL,
    work_exp INTEGER NOT NULL DEFAULT 0,
    
    CONSTRAINT fk_employee_org 
        FOREIGN KEY (employee_org_id) 
        REFERENCES employee_orgs(id)
        ON DELETE RESTRICT
);

-- Заполняем данными
INSERT INTO employee_orgs(name) VALUES
('Отдел развития'),
('Отдел маркетинга'),
('Совет директоров');

INSERT INTO employees(fio, posts, employee_org_id, work_exp) VALUES
('bima', 'student, junior, middle, senior pomidor', 
(SELECT id FROM employee_orgs WHERE name = 'Отдел развития'), 5),
('ырунь', 'student, sheymbot, sheymless', 
(SELECT id FROM employee_orgs WHERE name = 'Отдел маркетинга'), 5);