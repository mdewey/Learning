(* Homework1 Simple Test *)
(* These are basic test cases. Passing these tests does not guarantee that your code will pass the actual homework grader *)
(* To run the test, add a new line to the top of this file: use "homeworkname.sml"; *)
(* All the tests should evaluate to true. For example, the REPL should say: val test1 = true : bool *)

use "homework1.sml";

(* is_older *)
val test11 = is_older((1,2,3),(2,3,4)) = true
val test12 = is_older((1,2,3),(1,3,4)) = true
val test13 = is_older((1,2,3),(1,2,4)) = true
(* dates are the same*)
val test14 = is_older((1,2,3),(1,2,3)) = false
(* date2 is prior to date1 *)
val test15 = is_older((2,3,4), (1,2,3)) = false
val test16 = is_older((1,3,4), (1,2,3)) = false
val test17 = is_older((1,2,4), (1,2,3)) = false

(*number in month*)

val test21 = number_in_month([(2012,1,28),(2013,12,1)],2) = 0
val test22 = number_in_month([(2012,2,28),(2013,12,1)],2) = 1
val test23 = number_in_month([(2012,2,28),(2013,2,1)],2) = 2
val test24 = number_in_month([(2012,2,28),(2014,4, 22),(2013,2,1)],2) = 2

(* number in months*)
val test31 = number_in_months([(2012,2,28),(2013,12,1),(2011,3,31),(2011,4,28)],[2,3,4]) = 3
val test32 = number_in_months([(2012,5,28),(2013,12,1),(2011,5,31),(2011,5,28)],[2,3,4]) = 0

(* date in month*)

val test40 = dates_in_month([(2012,2,28),(2013,12,1)],5) = []
val test41 = dates_in_month([(2012,2,28),(2013,12,1)],2) = [(2012,2,28)]
val test42 = dates_in_month([(2012,12,28),(2013,2,1)],2) = [(2013,2,1)]
val test43 = dates_in_month([(2012,2,28),(2013,2,1)],2) = [(2012,2,28), (2013, 2,1)]
val test44 = dates_in_month([(2012,2,28),(2015,3,3),(2013,2,1)],2) = [(2012,2,28), (2013, 2,1)]

(*dates in months*)

val test51 = dates_in_months([(2012,2,28),(2013,12,1),(2011,3,31),(2011,4,28)],[2,3,4]) = [(2012,2,28),(2011,3,31),(2011,4,28)]
val test52 = dates_in_months([(2012,2,28),(2013,12,1),(2011,3,31),(2011,4,28)],[6,7,8]) = []


(* get_nth*)

val test61 = get_nth(["hi", "there", "how", "are", "you"], 2) = "there"

(* date to string *)

val test71 = date_to_string((2013, 6, 1)) = "June 1, 2013"

(* number before reaching sum*)
val test80 = number_before_reaching_sum(10, [1,2,3,4,5]) = 3
val test81 = number_before_reaching_sum(11, [1,2,3,4,5]) = 4
val test84 = number_before_reaching_sum(1, [1,2,5,10]) = 0
val test85 = number_before_reaching_sum(2, [1,2,5,10]) = 1
val test86 = number_before_reaching_sum(4, [1,2,5,10]) = 2
val test87 = number_before_reaching_sum(9, [1,2,5,10]) = 3

(* what month *)

val test90 = what_month(70) = 3
val test91 = what_month(31) = 1
(* month range *)

val test10 = month_range(31, 34) = [1,2,2,2]

(* oldest *) 

val test111 = oldest([(2012,2,28),(2011,3,31),(2011,4,28)]) = SOME (2011,3,31)


val test111a = oldest([(2012,2,28),(2011,3,31),(2011,4,28)])

val test111b = is_older((2011,3,31), (2011, 4, 28))
val test111c = is_older((2012,3,31), (2011, 3, 31))
