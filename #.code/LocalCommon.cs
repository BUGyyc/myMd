public class LocalCommon
{
    private List<IList<int>> CheckNums(int[] nums, int target)
    {
        List<IList<int>> result = new List<IList<int>>();
        if (nums.Length <= 2) return result;
        Array.Sort(nums);
        int pre = 0;
        while (pre < nums.Length - 4)
        {
            for (int start = pre + 1; start < nums.Length - 3; start++)
            {
                int left = start + 1;
                int right = nums.Length - 1;
                while (left < right)
                {
                    if (target > nums[left] + nums[right])
                    {
                        left++;
                    }
                    else if (target < nums[left] + nums[right])
                    {
                        right--;
                    }
                    else
                    {
                        var list = new List<int>();
                        list.Add(nums[pre]);
                        list.Add(nums[start]);
                        list.Add(nums[left]);
                        list.Add(nums[right]);
                        result.Add(list);

                        //去除重复元素
                        while (left < right && nums[left] == list[1])
                            ++left;
                        while (left < right && nums[right] == list[2])
                            --right;
                    }
                }

            }
            //去除重复元素
            int currentStartNumber = nums[pre];
            while (pre < nums.Length - 2 && nums[pre] == currentStartNumber)
                ++pre;
        }
        return result;
    }

    private void ChangeArray(int[] nums)
    {
        int len = nums.Length;
        if (len <= 1) return;
        bool find = false;
        int i = 0;
        int j = 0;
        for (i = len - 1; i >= 0; i--)
        {
            for (j = i - 1; j >= 0; j--)
            {
                if (nums[i] > nums[j])
                {
                    find = true;
                    break;
                }
            }
        }
        if (find)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        else
        {
            List<int> list = new List<int> (nums);
            list.Reverse ();
            nums = list.ToArray ();
        }
    }
}