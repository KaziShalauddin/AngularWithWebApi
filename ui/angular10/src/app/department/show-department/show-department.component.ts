import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-show-department',
  templateUrl: './show-department.component.html',
  styleUrls: ['./show-department.component.css']
})
export class ShowDepartmentComponent implements OnInit {

    constructor(private service: SharedService) { }

    DepartmentList: any = [];

    ModalTitle:string;
    ActivateAddEditDepComp:boolean = false;
    dep:any;

    ngOnInit(): void {
      this.refreshDepartmentList(); 
    }

    addClick() {
      this.dep = {
          DepartmentId: 0,
          DepartmentName :""
        }
      this.ModalTitle = "Add Department";
      this.ActivateAddEditDepComp = true;
    }

    editClick(item) {
      this.dep = item;
      this.ModalTitle = "Edit Department";
      this.ActivateAddEditDepComp = true;
    }

    deleteClick(item) {
        if (confirm('Are you sure?')) {
            this.service.deleteDepartment(item.DepartmentId).subscribe(data => {
                alert(data.toString());
                this.refreshDepartmentList();
            });

        }
    }

    closeClick() {
        this.ActivateAddEditDepComp = false;
        this.refreshDepartmentList();
    }

    refreshDepartmentList() {
      this.service.getAllDepartments().subscribe(data => { this.DepartmentList = data });
    }
}