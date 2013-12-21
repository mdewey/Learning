fun is_older( date1 : (int * int* int), date2: (int * int * int))= 
    if #1 date1 < #1 date2
    then true
    else if #2 date1 < #2 date2
	 then true
	 else if #3 date1 < #3 date2
	      then true
	      else false

fun number_in_month (dates : (int * int * int) list , month : int) = 
    if null dates
    then 0 
    else
	(if #2 ( hd dates) = month then 1 else 0) + number_in_month( tl dates, month)


fun number_in_months (dates : (int * int * int) list, month: int list) = 
    if null month
then 0
else number_in_month(dates, hd month) + number_in_months(dates, tl month)
					 
fun dates_in_month (dates : (int * int * int) list , month : int) = 
    if null dates
    then []
    else 
	(if #2 (hd dates) = month then [hd dates] else []) @ dates_in_month(tl dates, month)

fun dates_in_months (dates : (int * int * int) list, month: int list) =
    if null month 
    then []
    else dates_in_month(dates, hd month) @ dates_in_months(dates, tl month)

fun get_nth (stuffs : string list, index : int) = 
    let fun countToNth( strings : string list, index : int, currentSpot : int) = 
        if index = currentSpot 
        then strings
        else countToNth ( tl strings, index,(currentSpot + 1) )
    in
	hd (countToNth(stuffs, index, 1))
    end

fun date_to_string (date : (int * int * int)) = 
   let
         val AllMonths = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
  in 
    (get_nth( AllMonths, (#2 date))) ^ " " ^ Int.toString(#3 date) ^ ", " ^ Int.toString(#1 date)
end


fun number_before_reaching_sum ( sum : int, ints: int list) = 
    let 
	fun helper(target: int, ints: int list, pos: int) = 
	    if null ints
            then 0 
	    else if (target - hd ints) > 0 
	         then helper(target - hd ints, tl ints, pos +1)
                 else pos 	  
    in 
      helper( sum, ints,0)
    end
	    

fun what_month (day: int) = 
   let
(*       val startdays = [31,59,90,120,151,181,212,243,273,304,334,365]*)
       val startdays = [31,28,31,30,31,30,31,31,31,31,30,31]
   in 
      1 +  number_before_reaching_sum(day, startdays) 
   end 

fun month_range (day1 : int, day2: int) = 
    if day1 = day2
    then [what_month(day1)]
    else what_month(day1) :: month_range(day1+1, day2)



(* known does not work *) 
fun oldest (dates : (int * int * int ) list) = 
    if null dates
    then NONE
    else
        let fun max_nonempty(dates : ( int * int * int) list ) = 
	    if null (tl dates)
            then hd dates
            else
		let val tl_ans = max_nonempty(tl dates)
		in 
		    if( is_older(hd dates, tl_ans))
		    then hd dates
	            else tl_ans
                end 
        in
	    SOME (max_nonempty(dates))
	end
