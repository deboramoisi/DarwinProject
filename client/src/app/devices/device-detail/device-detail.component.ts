import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Device } from 'src/app/_models/device';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { DevicesService } from 'src/app/_services/devices.service';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.css']
})
export class DeviceDetailComponent implements OnInit {
  device: Device;
  user: User;

  constructor(private devicesService: DevicesService, private route: ActivatedRoute, private toastr: ToastrService,
        public accountService: AccountService) { }

  ngOnInit(): void {
    this.loadDevice();
    this.loadUser();
  }

  loadDevice() {
    this.devicesService.getDevice(parseInt(this.route.snapshot.paramMap.get('id'))).subscribe(device => {
      this.device = device;
    })
  }

  loadUser() {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  toggleAssignationDevice(event) {

    if (event.target.checked) { 
      this.devicesService.assignDevice(event.target.value).subscribe((device: Device) => {
        this.toastr.success("Device assigned successfully");
        this.device = device;
      })
    } else {
      this.devicesService.unassignDevice(event.target.value).subscribe((device: Device) => {
        this.toastr.success("Device unassigned successfully"); 
        this.device = device;
      }) 
    }
    
  }

}
