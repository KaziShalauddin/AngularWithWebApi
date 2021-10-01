import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-add-edit-department',
  templateUrl: './add-edit-department.component.html',
  styleUrls: ['./add-edit-department.component.css']
})
export class AddEditDepartmentComponent implements OnInit {

    constructor(private service:SharedService) { }

    @Input() dep:any;
    DepartmentId:string;
    DepartmentName:string;

    ngOnInit(): void {
        this.DepartmentId = this.dep.DepartmentId;
        this.DepartmentName = this.dep.DepartmentName;
    }

    addDepartment() {
        var val = {
            DepartmentId : this.DepartmentId,
            DepartmentName: this.DepartmentName
        }
        this.service.addDepartment(val).subscribe(res => {
          alert(res.toString());
        });
    }

    updateDepartment() {
      var val = {
        DepartmentId: this.DepartmentId,
        DepartmentName: this.DepartmentName
      }
        this.service.updateDepartment(val).subscribe(res => {
        alert(res.toString());
      });
    }
}
