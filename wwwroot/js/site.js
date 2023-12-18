const uri = 'api/company';
let companies = [];
let isToDelete = false;
let collectionWrapper;
let  elems =[];
let updateId = null;



function getAllCompanies() {

  if(collectionWrapper != undefined){
  
    isToDelete = !isToDelete;
    document.body.removeChild(collectionWrapper);
  }
    fetch(uri)
      .then(response => response.json())
      .then(data => _displayItems(data,isToDelete))
      .catch(error => console.error('Unable to get items.', error));
      
  }

  function addCompany() {
    const addCompanyNameTextbox = document.getElementById('add-name');
    const helperText = document.getElementById('helperText');
    console.log(companies.some(company => company.name == addCompanyNameTextbox.value))
    if ( companies.some(company => company.name == addCompanyNameTextbox.value) ) {

      helperText.innerHTML = "Already taken";   
    }
    else if (addCompanyNameTextbox.value === '' ||  addCompanyNameTextbox.value == null) {
      helperText.innerHTML = "Cannot be Empty";   
    } else {
    const company = {
        name: addCompanyNameTextbox.value.trim()
      
    };
    fetch(uri, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(company)
    })
      .then(response => response.json())
      .then(() => {
        location.reload()
        addCompanyNameTextbox.value = '';
        getAllCompanies();
    
      })
      .catch(error => console.error('Unable to add company.', error));
  }
  }
  function deleteCompany(id) {
    fetch(`${uri}/${id}`, {
      method: 'DELETE'
    })
    .then(() => getAllCompanies())
    .catch(error => console.error('Unable to delete item.', error));
  }
  function openModal(id){
    updateId = id;
    var instance = M.Modal.getInstance(document.getElementById('modalUpdate'));
    instance.open();
  }

  function editCompany() {
    const editCompanyNameTextbox = document.getElementById('edit-name');
    const helperText = document.getElementById('helperTextUpdate');
    if ( companies.some(company => company.name == editCompanyNameTextbox.value) ) {

      helperText.innerHTML = "Already taken";   
    }
    else if (editCompanyNameTextbox.value === '' ||  editCompanyNameTextbox.value == null) {
      helperText.innerHTML = "Cannot be Empty";   
    } else {
    const updatedCompany = {
        id: updateId,
        name: editCompanyNameTextbox.value.trim()
    }
    fetch(`${uri}`, {
      method: 'PUT', 
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedCompany)
    })
    .then(response => {
      if (!response.ok) {
        throw new Error('Failed to update company.');
      }
    })
    .then(updatedCompany => {
      console.log('Company updated successfully:', updatedCompany);
      location.reload()
      editCompanyNameTextbox.value = '';
      getAllCompanies();
    })
    .catch(error => console.error('Unable to update company.', error));
  }
}



  function _displayItems(data, isToDelete) {
    collectionWrapper = document.createElement('ul');
    collectionWrapper.classList.add('collection');
  data.forEach(company => {
  
    const collectionItem = document.createElement('a');
    collectionItem.classList.add('collection-item', 'avatar');
    
  
    const image = document.createElement('i');
    image.classList.add('material-icons', 'circle', 'blue', 'btn-floating');
    image.setAttribute('onclick',`loadCompanyEmployeePage(${company.id})`)
    image.innerHTML = 'business';
   
    const title = document.createElement('span');
    title.classList.add('title');
    title.innerHTML = `${company.name}`;
    collectionItem.appendChild(image);
    collectionItem.appendChild(title);

    const editbutton = document.createElement('a')
    editbutton.classList.add('secondary-content','btn-floating');
    editbuttonicon = document.createElement('i');
    editbuttonicon.classList.add('material-icons','yellow','darken-3');
    editbuttonicon.innerHTML = "edit";
    editbutton.setAttribute('onclick',`openModal(${company.id})`)


    editbutton.appendChild(editbuttonicon);
    collectionItem.appendChild(editbutton);

   
    if(isToDelete == true){

      const deletebutton = document.createElement('a')
      deletebutton.classList.add('secondary-content','btn-floating');
      deletebutton.setAttribute('onclick',`deleteCompany(${company.id})`)
      delebuttonicon = document.createElement('i');
      delebuttonicon.classList.add('material-icons','red','darken-3');
      delebuttonicon.innerHTML = "delete";

      deletebutton.appendChild(delebuttonicon);
      collectionItem.appendChild(deletebutton);

      }
    
    
    collectionWrapper.appendChild(collectionItem);
    
    
    
  });
  document.body.appendChild(collectionWrapper);
  
  companies = data;
  getAllCompanyNames();
  
  function getAllCompanyNames(){
    elems = [];
    data.forEach(company => {
      elems.push(company.name)
    });
    return elems;
  }

  }
  function loadEmployeePage(){
    
    window.open(`employees.html?data=${encodeURIComponent(JSON.stringify(elems))}`, '_self');
    
    window.onload = getEmployees;
    
    
    
}
function loadCompanyEmployeePage(companyId){
    
  window.open(`employees.html?companyId=${companyId}&data=${encodeURIComponent(JSON.stringify(elems))}`, '_self');
  
  window.onload = getEmployees;
  
  
  
}

 