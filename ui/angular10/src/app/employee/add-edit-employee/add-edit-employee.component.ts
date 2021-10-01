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
  PhotoPath: string;

  DepartmentsList: any = [];

  ngOnInit(): void {
    this.loadAllDepartmentList();
    this.EmployeeId = this.employee.EmployeeId;
    this.EmployeeName = this.employee.EmployeeName;
    this.DepartmentId = this.employee.DepartmentId;
    this.JoiningDate = this.employee.JoiningDate;
    this.PhotoPath = this.employee.PhotoPath;
    this.PhotoFileName = this.employee.PhotoPath;
      this.PhotoFilePath = this.service.PhotoUrl + "/" + this.employee.PhotoPath;
  }

  addEmployee() {
    var val = {
      EmployeeId: this.EmployeeId,
      EmployeeName: this.EmployeeName,
      DepartmentId: this.DepartmentId,
      JoiningDate: this.JoiningDate,
      PhotoPath: this.PhotoFileName,
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
      JoiningDate: this.JoiningDate,
      PhotoPath: this.PhotoFileName,
    }
    this.service.updateEmployee(val).subscribe(res => {
      alert(res.toString());
    });
  }

  loadAllDepartmentList() {
    this.service.getAllDepartments().subscribe(data => { this.DepartmentsList = data });
  }

  uploadPhoto(event) {
    var file = event.target.files[0];
    const formData: FormData = new FormData();
    formData.append('EmployeePhoto', file, file.Name);

    this.service.uploadPhoto(formData).subscribe((data: any) => {
      this.PhotoFileName = data.toString();
      this.PhotoFilePath = this.service.PhotoUrl + "/" + this.PhotoFileName;
    });
  }
}
