import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-show-employee',
  templateUrl: './show-employee.component.html',
  styleUrls: ['./show-employee.component.css']
})
export class ShowEmployeeComponent implements OnInit {

  constructor(private service: SharedService) { }

  EmployeeList: any = [];

  ModalTitle: string;
  ActivateAddEditEmployeeComp: boolean = false;
  employee: any;

  ngOnInit(): void {
    this.refreshEmployeeList();
  }

  addClick() {
    this.employee = {
        EmployeeId: 0,
        EmployeeName: "",
        Department: "",
        JoiningDate: "",
        PhotoFileName: "nophoto.jpg",
    }
    this.ModalTitle = "Add Employee";
    this.ActivateAddEditEmployeeComp = true;
  }

  editClick(item) {
    this.employee = item;
    this.ModalTitle = "Edit Employee";
    this.ActivateAddEditEmployeeComp = true;
  }

  deleteClick(item) {
    if (confirm('Are you sure?')) {
      this.service.deleteEmployee(item.EmployeeId).subscribe(data => {
        alert(data.toString());
        this.refreshEmployeeList();
      });

    }
  }

  closeClick() {
    this.ActivateAddEditEmployeeComp = false;
    this.refreshEmployeeList();
  }

    refreshEmployeeList() {
        this.service.getEmployeeList().subscribe(data => { this.EmployeeList = data });
  }
}
