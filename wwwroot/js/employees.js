const uri = 'api/employee';
let employees = [];
let isToDelete = false;
let collectionWrapper;
let companyNamesObjects;
let companyId = null; 
let updateId = null;

const urlParams = new URLSearchParams(window.location.search);

window.onload = function() {
    
    if(urlParams.has('companyId')){
    companyId = urlParams.get('companyId');
   let companyName;
    fetch(`api/company/${companyId}`)
          .then(response => response.json())
          .then(data => {
            companyName = data;
            document.getElementById('titleHeader').innerHTML = "Employees at " + companyName.name;

            document.getElementById('deleteButton').setAttribute("onclick","getOnlyEmployeesAtCompany()" )

          })
          .catch(error => console.error('Unable to get items.', error));
    getOnlyEmployeesAtCompany();
    } 
if(urlParams.has('data')){
    const serializedArray = urlParams.get('data');
    // Parse the serialized array back into an actual array
     companyNames = JSON.parse(decodeURIComponent(serializedArray));
      companyNamesObjects = companyNames.reduce((acc, name) => {
      acc[name] = null; // Assigning null as the value for each name
      return acc;
    }, {});
  }else {
    document.getElementById('deleteButton').setAttribute("onclick","getAllEmployees()" )
    getAllEmployees();}


    
    $(document).ready(function(){
      $('input.autocomplete').autocomplete({
        minLength: 0,
        data: companyNamesObjects
      });
    });

  
};

function getAllEmployees() {
  
    if(collectionWrapper != undefined){
      isToDelete = !isToDelete;
      document.body.removeChild(collectionWrapper);
    }
      fetch(uri)
        .then(response => response.json())
        .then(data => showEmployees(data,isToDelete))
        .catch(error => console.error('Unable to get items.', error));
        
    }

    function getOnlyEmployeesAtCompany() {
    
      if(collectionWrapper != undefined){
        isToDelete = !isToDelete;
        document.body.removeChild(collectionWrapper);
      }
      fetch(`${uri}/company/${companyId}`)
          .then(response => response.json())
          .then(data => showEmployees(data,isToDelete))
          .catch(error => console.error('Unable to get items.', error));
          
      }

    function addEmployee() {
      
        const employeeGivenName = document.getElementById('givenName');
        
        const employeeSurName = document.getElementById('surName');
        const employeeCompany = document.getElementById('company');
        const employeeCompanyRole = document.getElementById('companyRole');
        const employeeEmail = document.getElementById('email');
        const employeeGender = document.getElementById('gender');
        const genderValue = employeeGender.value == "male" ? Genders.Male: employeeGender.value == 'female' ? Genders.Female :
        Genders.Other; 
        if (employeeGivenName.value === '' ||  employeeGivenName.value == null) {
          helperTextGiven.innerHTML = "Cannot be Empty";   
        } if (employeeSurName.value === '' ||  employeeSurName.value == null) {
          helperTextSur.innerHTML = "Cannot be Empty";   
        } if (employeeCompany.value === '' ||  employeeCompany.value == null) {
          helperTextCompany.innerHTML = "Cannot be Empty";   
        } if (employeeCompanyRole.value === '' ||  employeeCompanyRole.value == null) {
          helperTextCompanyRole.innerHTML = "Cannot be Empty";   
        } if (employeeGender.value == 'Gender' ||  employeeGender.value == null) {
          helperTextGender.innerHTML = "Must choose one";   
        } else {
      
        const employee = {
            GivenName: employeeGivenName.value,
            SurName: employeeSurName.value,
            Company: employeeCompany.value,
            CompanyRole: employeeCompanyRole.value,
            Email: employeeEmail.value,
            Gender: genderValue
        };  
        fetch(uri, {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(employee)
        })
          .then(response => response.json())
          .then(() => {
            if(companyId != null){
              location.reload();
              getOnlyEmployeesAtCompany();
            } else {
              location.reload();
            getAllEmployees();
            
            }
          })
          .catch(error => console.error('Unable to add company.', error));
      }
    }
      function editEmployee() {

        const updatedEmployeeGivenName = document.getElementById('updateGivenName');
        const updatedEmployeeSurName = document.getElementById('updateSurName');
        const updatedEmployeeCompany = document.getElementById('updateCompany');
        const updatedEmployeeCompanyRole = document.getElementById('updateCompanyRole');
        const updatedEmployeeEmail = document.getElementById('updateEmail');
        const updatedEmployeeGender = document.getElementById('updateGender');

        if (updatedEmployeeGivenName.value === '' ||  updatedEmployeeGivenName.value == null) {
          updatehelperTextGiven.innerHTML = "Cannot be Empty";   
        } if (updatedEmployeeSurName.value === '' ||  updatedEmployeeSurName.value == null) {
          updatehelperTextSur.innerHTML = "Cannot be Empty";   
        } if (updatedEmployeeCompany.value === '' ||  updatedEmployeeCompany.value == null) {
          updatehelperTextCompany.innerHTML = "Cannot be Empty";   
        } if (updatedEmployeeCompanyRole.value === '' ||  updatedEmployeeCompanyRole.value == null) {
          updatehelperTextCompanyRole.innerHTML = "Cannot be Empty";   
        } if (updatedEmployeeGender.value == 'Gender' ||  updatedEmployeeGender.value == null) {
          updatehelperTextGender.innerHTML = "Must choose one";   
        } else {

        const updatedGenderValue = updatedEmployeeGender.value == "male" ? Genders.Male: employee.gender.value == 'female' ? Genders.Female :
        Genders.Other; 
      
        const updatedEmployee = {
            Id: updateId,
            GivenName: updatedEmployeeGivenName.value,
            SurName: updatedEmployeeSurName.value,
            Company: updatedEmployeeCompany.value,
            CompanyRole: updatedEmployeeCompanyRole.value,
            Email: updatedEmployeeEmail.value,
            Gender: updatedGenderValue

            
          
        };
        console.log(updatedEmployee);
        fetch(`${uri}`, {
          method: 'PUT', 
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(updatedEmployee)
        })
        .then(response => {
          if (!response.ok) {
            throw new Error('Failed to update employee.');
          }
        })
        .then(updatedCompany => {
          console.log('Employee updated successfully:', updatedEmployee);
          location.reload()
          if(companyId != null){
            getOnlyEmployeesAtCompany();
          } else {
          getAllEmployees();
          }
        })
        .catch(error => console.error('Unable to update company.', error));
      }
    }
    
      function openModal(id){
        updateId = id;
        employee = employees.find(e => e.id === updateId);
        console.log(employee.gender);

        document.getElementById('updateGivenName').value = employee.givenName;
        document.getElementById('updateSurName').value = employee.surname;
        document.getElementById('updateCompany').value = employee.company;;
        document.getElementById('updateCompanyRole').value = employee.companyRole;;
        document.getElementById('updateEmail').value = employee.email;
        
        var instance = M.Modal.getInstance(document.getElementById('modalUpdate'));
        instance.open();
      }
      function deleteEmployee(id) {
        fetch(`${uri}/${id}`, {
          method: 'DELETE'
        })
        .then(() =>  {if(companyId != null){
          getOnlyEmployeesAtCompany();
        } else {
        getAllEmployees();
        }})
        .catch(error => console.error('Unable to delete item.', error));
      }

    function showEmployees(data) {
      
    
        collectionWrapper = document.createElement('ul');
        collectionWrapper.classList.add('collection');
        console.log(data);
      data.forEach(employee => { 
      
        const collectionItem = document.createElement('a');
        collectionItem.classList.add('collection-item', 'avatar');
        
      
        const image = document.createElement('i');
       image.classList.add('material-icons', 'circle', 'blue');
       image.innerHTML = 'face';
  
        const title = document.createElement('span');
        title.classList.add('title');
        title.innerHTML = `${employee.givenName} ${employee.surname}`;
        const paragraph = document.createElement('p');
        paragraph.innerHTML = `${employee.company}, ${employee.companyRole} <br> ${employee.email} <br> ${employee.gender == 0 ? "Female": employee.gender == 1 ? "Male": "Other" }`
        collectionItem.appendChild(image);
        collectionItem.appendChild(title);
        collectionItem.appendChild(paragraph);
        

        const editbutton = document.createElement('a')
        editbutton.classList.add('secondary-content','btn-floating');
        editbuttonicon = document.createElement('i');
        editbuttonicon.classList.add('material-icons','yellow','darken-3');
        editbuttonicon.innerHTML = "edit";
        editbutton.setAttribute('onclick',`openModal(${employee.id})`)
    
        editbutton.appendChild(editbuttonicon);
        collectionItem.appendChild(editbutton);

        if(isToDelete == true){
          const deletebutton = document.createElement('a')
          deletebutton.classList.add('secondary-content','btn-floating');
          deletebutton.setAttribute('onclick',`deleteEmployee(${employee.id})`)
          delebuttonicon = document.createElement('i');
          delebuttonicon.classList.add('material-icons','red','darken-3');
          delebuttonicon.innerHTML = "delete";
          deletebutton.appendChild(delebuttonicon);
          collectionItem.appendChild(deletebutton);
          collectionItem.removeAttribute("href");
          }
        
        
        collectionWrapper.appendChild(collectionItem);
        
        
        
      });
      document.body.appendChild(collectionWrapper);
      employees = data;
      
      }
      
      const Genders = {
        Female: 0,
        Male: 1,
        Other: 2
    };