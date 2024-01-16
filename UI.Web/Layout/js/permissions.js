// CREATED BY MAHMOUD TAHOON
// 27 - May - 2007
// This file contains the groups definition for every permission type, grouped
// by system
/////////////////////////////////////////////////////////////////////////////////////
var groups = new Array();
	        // System 1
	        /*show   */groups[0]=["chkPg16Show","chkPg7Show","chkPg17Show","chkPg18Show"]; 
	        /*modify */groups[1]=["chkPg16Modify","chkPg7Modify","chkPg17Modify","chkPg18Modify"]; 
	        /*delete */groups[2]=["chkPg7Delete","chkPg17Delete"]; 
	        /*full   */groups[3]=["chkSys1Show","chkSys1Modify","chkSys1Delete","chkSys1DeleteAll"]; 
	        	        
	        // System 2
	        /*show   */groups[4]=["chkPg1Show" ,"chkPg2Show","chkPg3Show","chkPg4Show","chkPg5Show","chkPg6Show","chkPg8Show","chkPg9Show","chkPg10Show","chkPg11Show"];
	        /*modify */groups[5]=["chkPg11Modify","chkPg10Modify","chkPg9Modify","chkPg8Modify","chkPg1Modify","chkPg2Modify","chkPg3Modify","chkPg4Modify","chkPg5Modify","chkPg6Modify"];
	        /*delete */groups[6]=["chkPg3Delete","chkPg11Delete","chkPg10Delete","chkPg9Delete","chkPg8Delete","chkPg6Delete","chkPg5Delete","chkPg4Delete","chkPg1Delete","chkPg2Delete"]; 
	        /*full   */groups[7]=["chkSys2Show","chkSys2Modify","chkSys2Delete","chkSys2DeleteAll"];
	        
	        // System 3
	        /*show   */groups[8]=["chkPg12Show","chkPg13Show","chkPg14Show","chkPg15Show"];
	        /*modify */groups[9]=["chkPg12Modify","chkPg13Modify","chkPg14Modify"];
	        /*delete */groups[10]=["chkPg12Delete","chkPg13Delete","chkPg14Delete"];
	        /*full   */groups[11]=["chkSys3Show","chkSys3Modify","chkSys3Delete","chkSys3DeleteAll"];
	        
	        // System 4
	        /*show   */groups[12]=["chkPg31Show","chkPg32Show","chkPg33Show","chkPg34Show","chkPg35Show","chkPg36Show","chkPg37Show","chkPg38Show","chkPg39Show","chkPg40Show","chkPg41Show"];
	        /*modify */groups[13]=["chkPg31Modify","chkPg32Modify","chkPg33Modify","chkPg34Modify","chkPg35Modify","chkPg36Modify","chkPg37Modify","chkPg38Modify","chkPg39Modify"];
	        /*delete */groups[14]=["chkPg31Delete","chkPg31Date","chkPg32Delete","chkPg33Delete","chkPg33Date","chkPg34Delete","chkPg35Delete","chkPg35Date","chkPg36Delete","chkPg36Date","chkPg37Delete","chkPg38Delete","chkPg38Date","chkPg39Delete","chkPg39Date"];
	        /*full   */groups[15]=["chkSys4Show","chkSys4Modify","chkSys4Delete","chkSys4DeleteAll"];
	        
	        // System 5
	        /*show   */groups[16]=["chkPg19Show","chkPg20Show","chkPg21Show","chkPg22Show","chkPg23Show"];
	        /*modify */groups[17]=["chkPg19Modify","chkPg20Modify","chkPg21Modify","chkPg22Start","chkPg22Recieve","chkPg23Start","chkPg23Recieve"];
	        /*delete */groups[18]=["chkPg19Delete","chkPg20Delete","chkPg21Delete","chkPg22Date"];
	        /*full   */groups[19]=["chkSys5Show","chkSys5Modify","chkSys5Delete","chkSys5DeleteAll"];
	        
	        // System 6
	        /*show   */groups[20]=["chkPg24Show","chkPg25Show","chkPg26Show","chkPg27Show","chkPg28Show","chkPg29Show","chkPg30Show","chkPg30ShowVoucher"];
	        /*modify */groups[21]=["chkPg24Modify","chkPg25Modify","chkPg27Modify","chkPg28Modify","chkPg30ModifyVoucher"];
	        /*delete */groups[22]=["chkPg27Date","chkPg24Delete","chkPg25Delete","chkPg25Credits","chkPg27Delete","chkPg29Delete","chkPg30AddVoucher"];
	        /*full   */groups[23]=["chkSys6Show","chkSys6Modify","chkSys6Delete","chkSys6DeleteAll"];
	        
	        /*delete-all 1   */groups[24]=["chkPg7DeleteAll","chkPg17DeleteAll"];
	        /*delete-all 2   */groups[25]=["chkPg1DeleteAll","chkPg2DeleteAll","chkPg3DeleteAll","chkPg4DeleteAll","chkPg5DeleteAll","chkPg6DeleteAll"
	                                      ,"chkPg8DeleteAll","chkPg9DeleteAll","chkPg10DeleteAll","chkPg11DeleteAll"];
	        /*delete-all 3   */groups[26]=["chkPg12DeleteAll","chkPg13DeleteAll","chkPg14DeleteAll"];
	        /*delete-all 4   */groups[27]=["chkPg31DeleteAll","chkPg32DeleteAll","chkPg33DeleteAll","chkPg34DeleteAll","chkPg35DeleteAll"
							               ,"chkPg36DeleteAll","chkPg37DeleteAll","chkPg38DeleteAll","chkPg39DeleteAll"];
	        /*delete-all 5   */groups[28]=["chkPg19DeleteAll","chkPg20DeleteAll","chkPg21DeleteAll","chkPg22DeleteDone","chkPg22DeleteWait","chkPg23DeleteDone","chkPg23DeleteWait"];
	        /*delete-all 6   */groups[29]=["chkPg24DeleteAll","chkPg25DeleteAll","chkPg27DeleteAll","chkPg29DeleteAll","chkPg30DeleteAllVoucher","chkPg28Delete","chkPg30Delete"];
	        
	        groups[30]=["chkSys7Show","chkSys7Modify","chkSys7Delete","chkSys7DeleteAll"];
	        groups[31]=["chkPg42Recieve","chkPg42Show"];
	        groups[32]=["chkPg42Modify"];
	        groups[33]=["chkPg42Delete"];
	        groups[34]=["chkPg42DeleteAll","chkPg42Date"];
	        

function LooopSystems()
	        {
	            var chk = document.getElementById("chkSys1Show");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,0);
	            }
	            chk = document.getElementById("chkSys1Modify");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,1);
	            }
	            chk = document.getElementById("chkSys1Delete");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,2);
	            }
	            
	            chk = document.getElementById("chkSys2Show");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,4);
	            }
	            chk = document.getElementById("chkSys2Modify");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,5);
	            }
	            chk = document.getElementById("chkSys2Delete");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,6);
	            }
	            
	            chk = document.getElementById("chkSys3Show");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,8);
	            }
	            chk = document.getElementById("chkSys3Modify");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,9);
	            }
	            chk = document.getElementById("chkSys3Delete");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,10);
	            }
	            	            
	            chk = document.getElementById("chkSys4Show");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,12);
	            }
	            chk = document.getElementById("chkSys4Modify");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,13);
	            }
	            chk = document.getElementById("chkSys4Delete");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,14);
	            }
	            	            
	            chk = document.getElementById("chkSys5Show");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,16);
	            }
	            chk = document.getElementById("chkSys5Modify");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,17);
	            }
	            chk = document.getElementById("chkSys5Delete");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,18);
	            }
	            	            
	            chk = document.getElementById("chkSys6Show");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,20);
	            }
	            chk = document.getElementById("chkSys6Modify");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,21);
	            }
	            chk = document.getElementById("chkSys6Delete");
	            //alert("CHK: "+chk+" CHECKED: "+chk.checked);
	            if( chk.checked )
	            {
	                //chk.click();
	                chkBoxClicked(chk,22);
	            }
	        }