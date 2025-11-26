drop table employees, employee_orgs;

--подразделения
CREATE TABLE employee_post (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR NOT NULL
);

--сотрудники
CREATE TABLE employees (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    fio VARCHAR NOT NULL,
    posts VARCHAR,
    employee_org_id UUID NOT NULL,
    work_exp INTEGER NOT NULL DEFAULT 0,
    
    CONSTRAINT fk_employee_org 
        FOREIGN KEY (employee_org_id) 
        REFERENCES employee_orgs(id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

select * from employees;
select * from employee_orgs