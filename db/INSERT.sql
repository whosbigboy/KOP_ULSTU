drop table employees, employee_orgs;

--подразделение
INSERT INTO employee_orgs(name) VALUES
('Отдел развития'),
('Отдел маркетинга'),
('Совет директоров');

--сотрудники
INSERT INTO employees(fio, posts, employee_org_id, work_exp) VALUES
('bima', 'student, junior, middle, senior pomidor', 
(SELECT id FROM employee_orgs WHERE name = 'Отдел развития'), 5),
('ырунь', 'student, sheymbot, sheymless', 
(SELECT id FROM employee_orgs WHERE name = 'Отдел маркетинга'), 5);

select * from employee_orgs;
select * from employees;