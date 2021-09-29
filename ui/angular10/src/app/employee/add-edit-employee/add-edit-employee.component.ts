import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-add-edit-employee',
  templateUrl: './add-edit-employee.component.html',
  styleUrls: ['./add-edit-employee.component.css']
})
export class AddEditEmployeeComponent implements OnInit {
  constructor(private service: SharedService) { }

  @Input() employee: any;
  EmployeeId: string;
  EmployeeName: string;
  DepartmentId: number;
  JoiningDate: string;
  PhotoFileName: string;
  PhotoFilePath: string;

  DepartmentsList: any = [];
  
  ngOnInit(): void {
    this.loadAllDepartmentList();
    this.EmployeeId = this.employee.EmployeeId;
    this.EmployeeName = this.employee.EmployeeName;
    //this.DepartmentId = this.employee.DepartmentId;
    this.DepartmentId = 1;
  }

  addEmployee() {
    var val = {
      EmployeeId: this.EmployeeId,
      EmployeeName: this.EmployeeName,
      DepartmentId: this.DepartmentId,
    }
    this.service.addEmployee(val).subscribe(res => {
      alert(res.toString());
    });
  }

  updateEmployee() {
    var val = {
      EmployeeId: this.EmployeeId,
      EmployeeName: this.EmployeeName,
      DepartmentId: this.DepartmentId,
    }
    this.service.updateEmployee(val).subscribe(res => {
      alert(res.toString());
    });
  }

    loadAllDepartmentList() {
        this.service.getAllDepartments().subscribe(data => { this.DepartmentsList = data });
  }
}
