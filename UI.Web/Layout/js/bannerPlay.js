var bannerTimer = null;
jQuery(document).ready(function($){
	var slider = {
		timer:null,
		delay:4000,
		start:function(){
			this.timer = setTimeout(function(){
				slider.next();
			}, this.delay);
			$('#bannerStop').removeClass("play");
		},
		stop:function(){
			$('#bannerStop').addClass("play");
			clearTimeout(this.timer);
		},
		prev:function(){
			this.stop();
			$('#slideImages .currentBanner').fadeOut(function(e){
				var next = $(this).removeClass('currentBanner').prev();
				$('#'+$(this).attr('id')+'-link').removeClass('panelThumb_active');
				$('#'+$(this).attr('id')+'-title').removeClass('active');
				if(!next.length){
					next = $('#slideImages .tabContent:last');
				}
				next.addClass('currentBanner').fadeIn();
				$('#'+next.attr('id')+'-link').addClass('panelThumb_active');
				$('#'+next.attr('id')+'-title').addClass('active');
			});
		},			
		next:function(manualy){				
			if(manualy){
				this.stop();
			}				
			$('#slideImages .currentBanner').fadeOut(function(e){				
				var next = $(this).removeClass('currentBanner').next();					
				$('#'+$(this).attr('id')+'-link').removeClass('panelThumb_active');
				$('#'+$(this).attr('id')+'-title').removeClass('active');
				if(!next.length){
					next = $('#slideImages .tabContent:first');
				}					
				next.addClass('currentBanner').fadeIn();
				$('#'+next.attr('id')+'-link').addClass('panelThumb_active');
				$('#'+next.attr('id')+'-title').addClass('active');
				if(!manualy){
					slider.start();
				}
			});
		},			
		goto:function(id){				
			this.stop();
			$('#slideImages .currentBanner').fadeOut(function(e){					
				$(this).removeClass('currentBanner');
				$('#'+$(this).attr('id')+'-link').removeClass('panelThumb_active');
				$('#'+$(this).attr('id')+'-title').removeClass('active');
				var next = $(document.getElementById(id));
				next.addClass('currentBanner').fadeIn();
				$('#'+next.attr('id')+'-link').addClass('panelThumb_active');
				$('#'+next.attr('id')+'-title').addClass('active');
			});
		}
	}		
	$('#slideImages a[href=#previous]').click(function(){
		slider.prev(true);
		return false;
	})
	$('#slideImages a[href=#next]').click(function(){
		slider.next(true);
		return false;
	})
	$('#bannerStop').click(function(){
		if($(this).hasClass('play')){
			slider.start();
		}else{
			slider.stop();
		}
		return false;
	})
	$('a.panelThumb').click(function(){
		var id = this.id.replace('-link', '');
		slider.goto(id);
		return false;			
	});
	slider.start();
});